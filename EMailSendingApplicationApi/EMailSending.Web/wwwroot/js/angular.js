var myApp = angular.module('myApp', []);


myApp.controller('MyController', function ($scope, $http) {
    $scope.apiLocation = 'https://localhost:44301/api';
    $scope.test="testtttttttttttttt222"
    $scope.fnAccount = function () {
        var obj =
        {
            form:
            {
                name: '',
                password: '',
                email: '',
            },
            save: function () {
                var fd = new FormData();

                fd.append('Name', obj.form.name);
                fd.append('Email', obj.form.email);
                fd.append('Password', obj.form.password);


                $http({
                    method: 'POST',
                    url: $scope.apiLocation + '/Account',
                    data: fd,
                    headers: {
                        'Content-Type': undefined,

                    }
                }).then(function successCallback(response) {

                }, function errorCallback(response) {

                });

            },

        }
        return obj;
    };

    $scope.account = $scope.fnAccount();


});
