(function (angular) {

    angular.module("mainModule").service("tasklistService",
        ['$http', 'routePrefix', '$window',
            function ($http, routePrefix, $window) {

                return {

                    getTasks: function (pageNumber, pageSize) {

                        return $http.get(routePrefix.task + "/" + pageNumber + "/" + pageSize);
                    },

                    getTasksBySearch: function (search, pageNumber, pageSize) {
                        return $http.get(routePrefix.task + "/GetByName/" + search + "/" + pageNumber + "/" + pageSize);
                    },

                    getTasksByStatus: function (status, pageNumber, pageSize) {
                        return $http.get(routePrefix.task + "/GetByStatus/" + status + "/" + pageNumber + "/" + pageSize);
                    },

                    getUsers: function(){
                        return $http.get(routePrefix.user);
                    },

                    addTask: function (task) {

                        var token = $window.localStorage.token;

                        return $http({
                            method: 'post',
                            url: routePrefix.task,
                            headers: { 'Authorization': 'Bearer ' + token },
                            data: task
                        });
                    },

                    editTask: function (task) {

                        var token = $window.localStorage.token;

                        return $http({
                            method: 'put',
                            url: routePrefix.task + "/update/" + task.Id,
                            headers: { 'Authorization': 'Bearer ' + token },
                            data: task
                        });
                    },

                    deleteTask: function (id) {

                        var token = $window.localStorage.token;

                        return $http.delete(routePrefix.task + "/delete/" + id,
                            {
                                headers: { 'Authorization': 'Bearer ' + token }
                            });
                    }
                }
            }
        ]);

})(angular);