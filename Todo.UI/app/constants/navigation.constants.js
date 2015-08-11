(function (angular) {

    angular.module("mainModule").constant("navigationLinks",
        {
            home: "#/",
            account: "#/account",
            tasklist: "#/tasklist",
            mytask: "#/mytask",
            
        });

    angular.module("mainModule").constant("routePrefix",
        {
           
            task: "todolist/api/todo",
            mytask: "todolist/api/todo",
            user: "todolist/api/user",
        });

})(angular);