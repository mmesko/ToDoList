(function (angular) {

    // Converts datetime to ago type, for example 5 days ago
    angular.module("mainModule").filter("fromNow", function () {

        return function (dateString) {
            return moment(dateString).fromNow();
            
        };
    });

})(angular);