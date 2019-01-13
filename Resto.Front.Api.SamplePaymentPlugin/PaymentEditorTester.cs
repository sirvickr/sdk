using System;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using Resto.Front.Api.V6;
using Resto.Front.Api.V6.Data.Orders;
using Resto.Front.Api.V6.Data.Payments;
using Resto.Front.Api.V6.Extensions;

namespace Resto.Front.Api.SamplePaymentPlugin
{
    internal sealed class PaymentEditorTester : IDisposable
    {
        private Window window;
        private ListBox buttons;

        public PaymentEditorTester()
        {
            var windowThread = new Thread(EntryPoint);
            windowThread.SetApartmentState(ApartmentState.STA);
            windowThread.Start();
        }

        private void EntryPoint()
        {
            buttons = new ListBox();
            window = new Window
            {
                Content = buttons,
                Width = 200,
                Height = 500,
                Topmost = true,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                ResizeMode = ResizeMode.CanMinimize,
                ShowInTaskbar = true,
                VerticalContentAlignment = VerticalAlignment.Center,
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                WindowStyle = WindowStyle.SingleBorderWindow,
            };

            AddButton("Add cash payment item", AddPaymentItem);
            AddButton("Add card payment item", AddCardPaymentItem);
            AddButton("Add credit payment item", AddCreditPaymentItem);
            AddButton("Add cash payment item with discount", AddPaymentItemWithDiscount);
            AddButton("Delete payment item", DeletePaymentItem);
            AddButton("Delete payment item with discount", DeletePaymentItemWithDiscount);
            AddButton("Pay order on cash", PayOrderOnCash);
            AddButton("Pay order with existing payments", PayOrder);

            window.ShowDialog();
        }

        private void AddButton(string text, Action action)
        {
            var button = new Button { Content = text, Margin = new Thickness(2) };
            button.Click += (s, e) =>
            {
                try
                {
                    action();
                }
                catch (Exception ex)
                {
                    var message = string.Format("{4} \n Cannot {0} :-({1}Message: {2}{1}{3}", text, Environment.NewLine, ex.Message, ex.StackTrace, ex.GetType());
                    MessageBox.Show(message, "PaymentEditorTester", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            };
            buttons.Items.Add(button);
        }

        /// <summary>
        /// Добавление отложенного платежа наличными.
        /// </summary>
        private void AddPaymentItem()
        {
            var order = PluginContext.Operations.GetOrders().Last(o => o.Status == OrderStatus.New);
            var paymentType = PluginContext.Operations.GetPaymentTypes().Last(x => x.Kind == PaymentTypeKind.Cash);
            PluginContext.Operations.AddPreliminaryPaymentItem(100, null, paymentType, order, PluginContext.Operations.GetCredentials());
        }

        /// <summary>
        /// Добавление отложенного платежа Diners.
        /// </summary>
        private void AddCardPaymentItem()
        {
            var order = PluginContext.Operations.GetDeliveryOrders().Last(o => o.Status == OrderStatus.New);
            var paymentType = PluginContext.Operations.GetPaymentTypes().Last(x => x.Kind == PaymentTypeKind.Card && x.Name.ToUpper() == "DINERS");
            var additionalData = new CardPaymentItemAdditionalData { CardNumber = "123456" };
            PluginContext.Operations.AddPreliminaryPaymentItem(150, additionalData, paymentType, order, PluginContext.Operations.GetCredentials());
        }

        /// <summary>
        /// Добавление отложенного платежа по контрагенту. Тип Безн*, контрагент BBB*.
        /// </summary>
        private void AddCreditPaymentItem()
        {
            var order = PluginContext.Operations.GetOrders().Last(o => o.Status == OrderStatus.New || o.Status == OrderStatus.Bill);
            var paymentType = PluginContext.Operations.GetPaymentTypes().First(x => x.Kind == PaymentTypeKind.Credit && x.Name.ToUpper().StartsWith("БЕЗН"));
            var user = PluginContext.Operations.GetUsers().Last(u => u.Name.ToUpper().StartsWith("BBB"));
            var additionalData = new CreditPaymentItemAdditionalData { CounteragentUserId = user.Id };
            PluginContext.Operations.AddPreliminaryPaymentItem(200, additionalData, paymentType, order, PluginContext.Operations.GetCredentials());
        }

        /// <summary>
        /// Добавление отложенного платежа наличными со скидкой из типа оплаты.
        /// </summary>
        private void AddPaymentItemWithDiscount()
        {
            var order = PluginContext.Operations.GetOrders().Last(o => o.Status == OrderStatus.New || o.Status == OrderStatus.Bill);
            var paymentType = PluginContext.Operations.GetPaymentTypes().Last(x => x.Kind == PaymentTypeKind.Cash && x.DiscountType != null);
            var editSession = PluginContext.Operations.CreateEditSession();
            editSession.AddDiscount(paymentType.DiscountType, order);
            editSession.AddPreliminaryPaymentItem(100, null, paymentType, order);
            PluginContext.Operations.SubmitChanges(PluginContext.Operations.GetCredentials(), editSession);
        }

        /// <summary>
        /// Удаление отложенного платежа.
        /// </summary>
        private void DeletePaymentItem()
        {
            var order = PluginContext.Operations.GetOrders().Last(o => o.Status == OrderStatus.New || o.Status == OrderStatus.Bill);
            var paymentItem = order.Payments.Last();
            PluginContext.Operations.DeletePaymentItem(paymentItem, order, PluginContext.Operations.GetCredentials());
        }

        /// <summary>
        /// Удаление отложенного платежа со скидкой из типа оплаты.
        /// </summary>
        private void DeletePaymentItemWithDiscount()
        {
            var order = PluginContext.Operations.GetOrders().Last(o => o.Status == OrderStatus.New || o.Status == OrderStatus.Bill);
            var paymentItem = order.Payments.Last(i => i.Type.DiscountType != null);
            var discountItem = order.Discounts.Last(d => d.DiscountType.Equals(paymentItem.Type.DiscountType));
            var editSession = PluginContext.Operations.CreateEditSession();
            editSession.DeleteDiscount(discountItem, order);
            editSession.DeletePreliminaryPaymentItem(paymentItem, order);
            PluginContext.Operations.SubmitChanges(PluginContext.Operations.GetCredentials(), editSession);
        }

        private void PayOrderOnCash()
        {
            var order = PluginContext.Operations.GetOrders().Last(o => o.Status == OrderStatus.New || o.Status == OrderStatus.Bill);
            var paymentType = PluginContext.Operations.GetPaymentTypesToPayOutOnUser().Last(x => x.Kind == PaymentTypeKind.Cash);
            // Payment is possible by user with opened personal session.
            var credentials = PluginContext.Operations.AuthenticateByPin("777");
            PluginContext.Operations.PayOrderAndPayOutOnUser(credentials, order, paymentType, order.ResultSum);
        }

        private void PayOrder()
        {
            var order = PluginContext.Operations.GetOrders().Last(o => o.Status == OrderStatus.New || o.Status == OrderStatus.Bill);
            // Payment is possible only by user with opened personal session.
            var credentials = PluginContext.Operations.AuthenticateByPin("777");
            PluginContext.Operations.PayOrder(credentials, order);
        }

        public void Dispose()
        {
            window.Dispatcher.InvokeShutdown();
            window.Dispatcher.Thread.Join();
        }
    }
}
