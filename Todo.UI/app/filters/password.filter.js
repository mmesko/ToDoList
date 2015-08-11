(function (angular) {


    // Converts string to password string, for example someName to ********
    angular.module("todoFilters", []).filter('password', function () {

        return function (input) {
            
            var string = toString(input);
            var stringLength = string.length;
            var passwordString = "";

            for (var i = 0; i < stringLength; i++) {
                passwordString += "*";
            }

            return passwordString;
        };
    });

})(angular)