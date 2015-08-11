
// Main Module
(function (angular) {



    // Configure routes
    angular.module("mainModule", ['ngRoute', 'ui.bootstrap', 'todoFilters']).config(config);


    function config($routeProvider) {

        $routeProvider
            .when('/', { templateUrl: 'app/home/home.html', controller: 'HomeController', controllerAs: 'vm'})
            .when('/account', { templateUrl: 'app/account/account.html', controller: 'AccountController', controllerAs: 'vm' })
            .when('/mytask', { templateUrl: 'app/mytask/mytask.html', controller: 'MytaskController', controllerAs: 'vm' })
            .when('/tasklist', { templateUrl: 'app/tasklist/tasklist.html', controller: 'TasklistController', controllerAs: 'vm' })
            .when('/task/:taskTitle/:taskId', {templateUrl: 'app/task/task.html', controller: 'TaskController', controllerAs: 'vm', reloadOnSearch:false})
            .otherwise({ redirectTo: '/' });
    }

})(angular);