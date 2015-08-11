(function (angular) {

    angular.module("mainModule").service("userService",
        ['$http', '$window', 'routePrefix',
            function ($http, $window, routePrefix) {


            return {

                getUserByUsername: function (username) {
                    return $http.get(routePrefix.user + "/" + username);
                },

                // Updates user username or email
                updateUser: function (user, password) {                                  

                    var token = $window.localStorage.token;

                    return $http({
                        method: 'put',
                        url: routePrefix.user + "/UpdateUserOrMail/" + user.Id,
                        headers: { 'Authorization': 'Bearer ' + token },
                        data: {
                            user: user,
                            password: password
                        }
                    });
                },

                updateUserPassword: function (userId, oldPassword, newPassword) {

                    var token = $window.localStorage.token;

                    return $http({
                        method: 'put',
                        url: routePrefix.user + "/updatePassword/" + userId,
                        headers: { 'Authorization': 'Bearer ' + token },
                        data: {
                            oldPassword: oldPassword,
                            newPassword: newPassword
                        }
                    })
                }
            };
        }]);

})(angular);