(function (angular) {

    angular.module("mainModule").controller("ModalLoginController",
        ['$scope', '$window', '$modal', '$log', 'authService','notificationService', '$route',
             function ($scope, $window, $modal, $log, authService, notificationService, $route) {

            var vm = $scope.vm = {};

            vm.user = { userName: null, password: null };

            // when opening modal
            $scope.open = function () {

                var modalInstance = $modal.open({
                    animation: true,
                    templateUrl: 'app/modals/login/login.html',
                    controller: 'OpenLoginModalCtrl',
                    windowClass: 'modal-style',
                    backdrop: true,
                    resolve: {
                        injectUserInfo: function () {
                            return vm.user;
                        }
                    }
                });

                // Called after closing modal
                modalInstance.result.then(function (userToLogin) {

                    login(userToLogin);

                }, function () {
                  //  $log.info('Modal dismissed at: ' + new Date());
                });
            };

            // PRIVATE
            // used to log in user
            var login = function (user) {
               
                authService.login(user).success(function (data, status, header, config) {

                    $window.localStorage.user = data.username;
                    $window.localStorage.token = data.access_token;
                    $window.localStorage.id = data.id;
                    $route.reload();

                }).error(function (data, status, header, config) {
                    notificationService.addLongNotification(data.error + " : " + data.error_description, false);
                });
            }
    }]);

})(angular);



// Used when modal is created 
(function (angular) {

    angular.module("mainModule").controller("OpenLoginModalCtrl",
        ['$scope', '$modalInstance', 'injectUserInfo',
             function ($scope, $modalInstance, injectUserInfo) {

            $scope.userInfo = injectUserInfo;

            // For cancel button
            $scope.cancel = function () {
                $modalInstance.dismiss('cancel');
            };

            // User login
            $scope.userLogin = function (user) {
                $modalInstance.close(user);
            };

        }]);


})(angular);