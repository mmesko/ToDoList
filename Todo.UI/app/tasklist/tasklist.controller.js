(function (angular) {
    

    angular.module("mainModule").controller("TasklistController",
        ['$scope', 'tasklistService', '$window', 'notificationService', '$location','$interval',
            function ($scope, tasklistService, $window, notificationService, $location, $interval) {

                var vm = $scope.vm = {};

                //#region Proporties
                vm.users = [];
                vm.showAddTask = false;
                vm.searchString = "";
                vm.pageNumber = 1;
                vm.showTable = false;
                vm.showEditView = false;
                vm.showDetails = false;                
                vm.selected = {};
                var pageSize = 5;
                vm.status=1;

                vm.taskToAdd = {
                    Title: "",
                    TaskDescription: "",                   
                    UserId: "",
                    IsCompleted: false,
                    DateCreated: null,                   
                };

                vm.tasks = [];
                vm.dateCreated = {
                    value: null,
                    opened: false
                };
                //#endregion
                
               
                //#region Date
                $scope.toggleMin = function() {
                    $scope.minDate = $scope.minDate ? null : new Date();
                };
                $scope.toggleMin();

                $scope.open = function ($event, picker) {
                    picker.opened = !picker.opened;
                };

                $scope.dateOptions = {
                    formatYear: 'yy',
                    startingDay: 1
                };

                $scope.formats = ['dd-MMMM-yyyy', 'yyyy/MM/dd', 'dd.MM.yyyy', 'shortDate'];
                $scope.format = $scope.formats[0];

                var tomorrow = new Date();
                tomorrow.setDate(tomorrow.getDate() + 1);
                var afterTomorrow = new Date();
                afterTomorrow.setDate(tomorrow.getDate() + 2);
                $scope.events =
                  [
                    {
                        date: tomorrow,
                        status: 'full'
                    },
                    {
                        date: afterTomorrow,
                        status: 'partially'
                    }
                  ];

                //#endregion

                //pagination
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


        
                // Click on create task button , that only shows form for new task
                vm.showAddTaskClick = function () {

                    if ($window.localStorage.id === "" || $window.localStorage.id === null) {

                        notificationService.addNotification("Please log in to add new task... ", false);
                        return;
                    };

                    vm.showAddTask = true;
                }

                tasklistService.getUsers().success(function (data) {

                    if (data.length > 0) {
                        vm.users = data;
                    }
                }).error(function (data) {
                    notificationService.addNotification(data, false);
                });


                // Get tasks/todos
                vm.get = function () {
                    

                    if (vm.searchString.length > 0) {

                        tasklistService.getTasksBySearch(vm.searchString, vm.pageNumber, pageSize).success(function (data) {
                            bindData(data, vm.users);

                        }).error(function (data) {

                        });
                    }
                    else {

                        tasklistService.getTasks(vm.pageNumber, pageSize).success(function (data) {
                            bindData(data, vm.users);

                           
                        }).error(function (data) {

                        });
                    }
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


                vm.getItemDetails = function (item) {
                    vm.tasks = [];
                    vm.task = item;
                    vm.showTable = false;
                    vm.showDetails = true;
                };

          
                vm.addTask = function () {
                    // Add values to task proporties
                    vm.taskToAdd.UserId = $window.localStorage.id;
                    vm.taskToAdd.AssignedToUserId = vm.taskToAdd.UserId;
                    vm.taskToAdd.DateCreated = vm.taskToAdd.DateCreated;

                    tasklistService.addTask(vm.taskToAdd).success(function (data) {

                        if (data > 0) {
                            notificationService.addNotification("Task created", true);
                            vm.showAddTask = false;
                            vm.searchString = "";
                            vm.get();
                        }
                        else
                            notificationService.addNotification("Error while creating task.", false);

                    }).error(function (data) {
                        notificationService.addNotification(data, false);
                    });
                }

                vm.showEdit = function (item) {
                    vm.selected = {};
                    vm.selected = item;
                    vm.showTable = true;
                    vm.showEditView = true;                   
                };


                vm.editTask = function (item) {
                    // If user is not logged in return 
                    if ($window.localStorage.id === null || $window.localStorage.id === "") {
                        notificationService.addNotification("Please log in.", false);
                        return;
                    }
                    vm.selected = {};                
                    vm.selected.Title = item.Title;
                    vm.selected.TaskDescription = item.TaskDescription;
                    vm.selected.DateCreated = item.DateCreated;
                    vm.selected.IsCompleted = item.IsCompleted;
                    vm.selected.Id = item.Id;
                    vm.selected.UserId = item.UserId;                  
                    vm.selected.AssignedToUserId = vm.userModel ? vm.userModel.Id : item.AssignedToUserId;                

                    tasklistService.editTask(vm.selected).success(function (data) {
                       vm.selected = data;
                        notificationService.addNotification("Description changes saved. Please refresh page to see changes.", true);
                        vm.showEdit();
                        
                    }).error(function (data) {
                        notificationService.addNotification("Error while trying to edit task.", false);
                    });
                };

                
                vm.delete = function (task) {

                    tasklistService.deleteTask(task.Id).success(function () {

                        notificationService.addNotification("Task deleted.", true);
                        vm.showEditView = false;
                        vm.showAddTask = false;
                        vm.get();

                    }).error(function (data) {
                        notificationService.addLongNotification(data, false);
                    });
                };

             
                // Navigates to task page with task title as parameter
                vm.goToTask = function (task) {
                    $location.path('/task/' + task.Title + "/" + task.Id);
                }

                vm.get();
            }
        ]);

})(angular);