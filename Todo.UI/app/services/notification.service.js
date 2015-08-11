(function (angular) {

    angular.module("mainModule").service("notificationService",
        ['$rootScope', '$timeout',
            function ($rootScope, $timeout) {

                var notification = {
                    cls: "",
                    msg: ""
                };

                function animateOnValueChange(time) {
                    // fade in 
                    jQuery("#notification").fadeIn(400);

                    // Keep message for amount of time before fading out
                    $timeout(function () {
                        jQuery("#notification").fadeOut(400);
                    }, time, false);
                };

                return {

                    returnNotification: function () {
                        return notification;
                    },

                    addNotification: function (message, success) {

                        // If true add alert success, if false add alert danger
                        if (success == true) {
                            notification.cls = "alert alert-success";
                            notification.msg = message;
                            animateOnValueChange(1500);
                        }
                        else if (success == false) {
                            notification.cls = "alert alert-danger";
                            notification.msg = message;
                            animateOnValueChange(1500);
                        };
                    },

                    addLongNotification: function (message, success) {

                        // If true add alert success, if false add alert danger
                        if (success == true) {
                            notification.cls = "alert alert-success";
                            notification.msg = message;
                            animateOnValueChange(2500);
                        }
                        else if (success == false) {
                            notification.cls = "alert alert-danger";
                            notification.msg = message;
                            animateOnValueChange(2500);
                        };
                    },

                };
            }]);

})(angular);