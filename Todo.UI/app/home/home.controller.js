(function (angular) {

    angular.module("mainModule").controller("HomeController", ['$scope','homeService', 'notificationService','navigationMenuService',
        function ($scope, homeService, notificationService, navigationMenuService) {

            var vm = $scope.vm = {};

            //#region Variables 

            vm.myInterval = 5000; 

            vm.slides = [
            {
                image: '../../images/menu1.png'
                
            },
            {
                image: '../../images/menu2.png'
               
            },
            {
                image: '../../images/menu3.png'
                
            }];

        }
    ]);

})(angular)