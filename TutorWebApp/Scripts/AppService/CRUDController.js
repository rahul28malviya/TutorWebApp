var app = angular.module("myModule", []);
app.controller("myController", function ($scope, $http) {

    $http.get('/Home/GetAll').then(function (response) {
        $scope.tutor = response.data;
    });

    $scope.savedata = function () {
        $http({
            method: 'POST',
            url: '/Home/AddEditDetails',
            data: $scope.Tutor
        }).then(function (d) {
            $scope.Tutor = null;
            alert(d.data);
            window.location.reload();
        }, function (d) {
            alert(d.data);
        });
    };

    $scope.Delete = function (id) {
        $http({
            method: 'GET',
            url: '/Home/Delete/' + id,
        }).then(function (d) {
            $scope.Tutor = null;
            alert(d.data);
            window.location.reload();
        }, function (d) {
            alert(d.data);
        });
    };

    $scope.Get = function (id) {
        $http({
            method: 'GET',
            url: '/Home/Get/' + id,
        }).then(function (d) {
            var userData = d.data;
            $scope.Tutor = userData;
        }, function (d) {
            alert(d.data);
        });
    };
});