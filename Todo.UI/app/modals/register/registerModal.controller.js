
(function (angular) {

    angular.module("mainModule").controller('ModalRegisterController',
        ['$scope', '$modal', '$log', 'authService', 'notificationService', '$window',
            function ($scope, $modal, $log, authService, notificationService, $window) {

                var vm = $scope.vm = {};

                vm.registration = {
                    userName: "",
                    email: "",
                    cart: {}
                };

                vm.password = "";
                vm.confirmPassword = "";

                // Open modal
                $scope.open = function () {

                    var modalInstance = $modal.open({
                        animation: true,
                        templateUrl: 'app/modals/register/register.html',
                        controller: 'OpenRegisterModalCtrl',
                        windowClass: 'modal-style',
                        backdrop: true,
                        resolve: {
                            injectRegistration: function () {
                                return vm.registration;
                            },
                            injectPassword: function () {
                                return vm.password;
                            },
                            injectConfirmPassword: function () {
                                return vm.confirmPassword;
                            }
                        }
                    });

                    // Called after closing modal, 
                    modalInstance.result.then(function (data) {

                        var user = data.registration;
                        var pass = data.password;
                        var cp = data.confirmPassword;

                        register(user, pass, cp);
                    });
                };

                // Private functions
                // Used to register user
                function register(item, pass, confirmPass) {

                    if (pass === confirmPass && pass.length >= 6) {

                        authService.saveRegistration(item, pass).success(function (data, status, header, config) {
                            notificationService.addNotification("Registration successful.", true);
                        }).error(function (data, status, header, config) {
                            notificationService.addLongNotification(data, false);
                        });
                    }
                    else {
                        notificationService.addNotifciation("Please check your password fields.", false);
                    }


                };

            }]);

})(angular);




// Used when modal is created 
(function (angular) {

    angular.module("mainModule").controller("OpenRegisterModalCtrl",
        ['$scope', '$modalInstance', 'injectRegistration', 'injectPassword', 'injectConfirmPassword',
        function ($scope, $modalInstance, injectRegistration, injectPassword, injectConfirmPassword) {

            $scope.data = {
                user: injectRegistration,
                password: injectPassword,
                confirmPassword: injectConfirmPassword
            }

            // For cancel button
            $scope.cancel = function () {
                $modalInstance.dismiss('cancel');
            };

            // User register
            $scope.registerUser = function (data) {
                $modalInstance.close(data);
            };

        }]);


})(angular);