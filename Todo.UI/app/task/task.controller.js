(function (angular) {

    angular.module("mainModule").controller("TaskController", [
        '$scope', '$routeParams', 'taskService', 'notificationService', '$window', '$location', '$anchorScroll',
            function ($scope, $routeParams, taskService, notificationService, $window, $location, $anchorScroll) {

                var vm = $scope.vm = {};

                //#region Proporties and variables


                var taskId = $routeParams.taskId;
                var pageSize = 5;

                vm.taskTitle = $routeParams.taskTitle;
                vm.pageNumber = 1;
               
                //#endregion

                //#region Methods
                //methods will be added later


                //#endregion

            }
    ]);

})(angular);