(function (angular) {

    angular.module("mainModule").controller("MytaskController",
        ['mytaskService', '$window', '$scope', 'notificationService', 'navigationMenuService', '$location',
            function (mytaskService, $window, $scope, notificationService, navigationMenuService, $location) {

                var vm = $scope.vm = {};
                //#region Properties
                vm.pageNumber = 1;
                vm.showTable = false;
                vm.tasks = [];
                vm.pageNumber = 1;
                vm.pageSize = 5;
                vm.showDetails = false;
               //#endregion


                //paging
                vm.next = function () {

                    vm.pageNumber++;
                    vm.get();
                };

                vm.back = function () {

                    vm.pageNumber--;

                    if (vm.pageNumber < 1)
                        vm.pageNumber = 1;
                    vm.get();
                }

                //task details
                vm.getItemDetails = function (item) {
                    vm.tasks = [];
                    vm.task = item;
                    vm.showTable = false;
                    vm.showDetails = true;

                };

                //get all users
                mytaskService.getUsers().success(function (data) {

                    if (data.length > 0) {
                        vm.users = data;
                    }
                }).error(function (data) {
                    notificationService.addNotification(data, false);
                });

               vm.get = function () {

                   mytaskService.getUserTasks($window.localStorage.id, vm.pageNumber, vm.pageSize).success(function (data) {
                        vm.tasks = data;
                      
                        bindData(data, vm.users);

                    }).error(function () {
                        notificationService.addNotification("Couldn't find any task.", false);
                    });
               };


               var bindData = function (data, users) {
                   vm.tasks = data;
                   for (var i = 0; i < vm.tasks.length; i++) {
                       for (var j = 0; j < vm.users.length; j++) {
                           if (vm.users[j].Id == vm.tasks[i].AssignedToUserId) {
                               vm.tasks[i].AssignedToUser = {
                                   Id: vm.users[j].Id,
                                   UserName: vm.users[j].UserName
                               };
                               break;
                           }
                       }
                   }
                   vm.showTable = true;
               };

                // Navigates to task page with task title as parameter
                vm.goToTask = function (task) {
                    $location.path('/task/' + task.Title + "/" + task.Id);
                }

                vm.get();

              
            }
        ]);

})(angular);