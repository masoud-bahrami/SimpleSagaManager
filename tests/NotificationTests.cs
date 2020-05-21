using System;
using Xunit;

namespace SimpleSagaManager.UnitTests
{
    public class NotificationTests
    {
        [Fact]
        public void If_No_Error_Or_Error_Message_Is_Received_It_Should_Not_Report_An_Error() {

            Assert.False(Notification().HasError());
        }
       
        [Fact]
        public void If_an_Exception_Of_A_Particular_Type_Is_Received_It_Must_Be_Correctly_Identified() {

            var notification = Notification();

            Assert.False(Notification().HasExceptionOf<Exception>());

            notification.AddError(new Exception());

            Assert.True(notification.HasExceptionOf<Exception>());
        }

        [Fact]
        public void If_AtLeast_An_Exception_Is_Received_It_Must_Be_Correctly_Identified() { 

            var notification = Notification();

            notification.AddError(new Exception());

            Assert.True(notification.HasError());
        }


        [Fact]
        public void If_AtLeast_An_Error_Is_Received_It_Must_Be_Correctly_Identified() {

            var notification = Notification();

            notification.AddError("An error occured");

            Assert.True(notification.HasError());
        }

        [Fact]
        public void If_Neither_Error_Nor_Exception_Received_ErrorMessage_Should_Be_Empty() {

            var notification = Notification();

            Assert.Equal(string.Empty, notification.Errors());
        }

        [Fact]
        public void Should_Successfully_Format_Error_And_Exception_Message() {

            var notification = Notification();
            notification.AddError("An error Occured!");
            Assert.Equal("An error Occured!", notification.Errors());

            notification = Notification();
            notification.AddError(new Exception("An exception Occured!"));
            Assert.Equal("An exception Occured!", notification.Errors());


            notification = Notification();
            notification.AddError("An error Occured!");
            notification.AddError(new Exception("An exception has Occured!"));
            Assert.Equal("An error Occured! - An exception has Occured!", notification.Errors());
        }

        private Notification Notification()
            => new Notification();
    }
}
