
(function (angular) {

    angular.module("mainModule").service("mytaskService", ['routePrefix', '$http', '$window',
    function (routePrefix, $http, $window) {
     
        return {
            getUserTasks: function (userId, pageNumber, pageSize) {

                var token = $window.localStorage.token;
               
                return $http({
                    method: 'get',
                    url: routePrefix.task + "/GetByUser/" + userId + "/" + pageNumber + "/" + pageSize,
                    headers: { 'Authorization': 'Bearer ' + token }
                }); 
            },

            getUsers: function () {
                return $http.get(routePrefix.user);
            }


           

         
        }
    }
    ]);

})(angular);