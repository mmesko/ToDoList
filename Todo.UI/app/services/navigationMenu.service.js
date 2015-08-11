

// Used for setting up navigation menu 
(function (angular) {


    angular.module("mainModule").service("navigationMenuService", ["navigationLinks", function(navigationLinks){

        var menus =
            [
                { name: "Home", link: navigationLinks.home , active: "active" },          // Link goes into ng-href , link it's just bootstrap class used for menu selected effect
                { name: "Task List", link: navigationLinks.tasklist, active: "" }
            ];

        return {
            
            // Pass index of item, and set item to active
            setMenuToActive: function (menuItemIndex) {             

                for (var i = 0; i < menus.length; i++) {
                    menus[i].active = "";
                };
                menus[menuItemIndex].active = "active";
            },

            getMenu: function () {
                return menus;
            }


        }
    }]);


})(angular);