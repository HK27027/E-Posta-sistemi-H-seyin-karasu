var myApp = angular.module('myApp', []);


myApp.controller('MyController', function ($scope, $http) {
    $scope.apiLocation = 'https://localhost:44301/api';
  
    $scope.fnAccount = function () {
        var obj =
        {
            form:
            {
                name: '',
                password: '',
                email: '',
            },
            list: [],
            data: [],
            info: [],
            count: 0,
            danger: false,
            succes: false,
            dangerText: {},
            succesText: {},

            save: function () {
                if (obj.form.email == undefined) {
                    return true;
                }
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
                    obj.data = response.data.item;
                    obj.danger = false;
                    obj.succes = true;
                    obj.succesText = {};
                    obj.succesText = response;
                    localStorage.removeItem('accountId');
                    localStorage.setItem('accountId', obj.data.id);
                    location.href = "/";
                }, function errorCallback(response) {
                    obj.danger = true;
                    obj.succes = false;
                    obj.dangerText = {};
                    obj.dangerText = response;
                });

            },
            login: function () {
                var fd = new FormData();
                
                fd.append('Email', obj.form.email);
                fd.append('Password', obj.form.password);


                $http({
                    method: 'POST',
                    url: $scope.apiLocation + '/Account/Login',
                    data: fd,
                    headers: {
                        'Content-Type': undefined,

                    }
                }).then(function successCallback(response) {
                    obj.data = response.data.item;
                    obj.danger = false;
                    obj.succes = true;
                    obj.succesText = {};
                    obj.succesText = response;
                    localStorage.removeItem('accountId');
                    localStorage.setItem('accountId', obj.data.id);
                    location.href = "/";
                }, function errorCallback(response) {
                    obj.danger = true;
                    obj.succes = false;
                    obj.dangerText = {};
                    obj.dangerText = response;
                });


            },
            loginControl: function () {
                var account = localStorage.getItem('accountId');
                if (account == null) {
                    location.href = "/Login";
                } else {
                    var fd = new FormData();

                    fd.append('Email', obj.form.email);
                    fd.append('Password', obj.form.password);


                    $http({
                        method: 'Get',
                        url: $scope.apiLocation + '/Account/'+account,
                        data: fd,
                        headers: {
                            'Content-Type': undefined,

                        }
                    }).then(function successCallback(response) {
                        obj.data = response.data.item;
                        obj.danger = false;
                       
                    }, function errorCallback(response) {
                      
                     
                    });
                }
               


            },
            logOut: function () {
                localStorage.removeItem('accountId');
                obj.data = [];
                location.href = '/Login';
            },


            accountInfo: function () {
                var account = localStorage.getItem('accountId');
                var fd = new FormData();
                console.log();
                fd.append('AccountId', account);
                $http({
                    method: 'POST',
                    url: $scope.apiLocation + '/account/AccountInfo',
                    data: fd,
                    headers: {
                     
                        'Content-Type': undefined,
                    }
                }).then(function successCallback(response) {

                    if (response.status == 200) {
                        obj.info = [];
                        obj.info.push(response.data.item);
                    } else {

                    }

                }, function errorCallback(response) {

                });
            },

        }
        return obj;
    };

    $scope.account = $scope.fnAccount();




    $scope.fnUsers = function () {
        var obj =
        {
            form:
            {
                name: '',
                password: '',
                email: '',
                search: '',
            },
            list: [],
            data: [],
            count: 0,
            danger: false,
            succes: false,
            dangerText: {},
            succesText: {},
            add: function () {
                $('#exampleModal').modal('show');
            },
            save: function () {
                var account = localStorage.getItem('accountId');
                var fd = new FormData();

                fd.append('Name', obj.form.name);
                fd.append('Email', obj.form.email);
                fd.append('Title', obj.form.title);
                fd.append('CompanyName', obj.form.companyName);
                fd.append('PhoneNumber', obj.form.phoneNumber);
                fd.append('AccountId', account);


                $http({
                    method: 'POST',
                    url: $scope.apiLocation + '/User',
                    data: fd,
                    headers: {
                        'Content-Type': undefined,

                    }
                }).then(function successCallback(response) {
                    obj.data = response.data.item;
                    obj.multipleGet();
                    obj.danger = false;
                    obj.succes = true;
                    obj.succesText = {};
                    obj.succesText = response;
                    $('#exampleModal').modal('hide');
                }, function errorCallback(response) {
                    obj.danger = true;
                    obj.succes = false;
                    obj.dangerText = {};
                    obj.dangerText = response;
                });

            },

            multipleGet: function () {
                var account = localStorage.getItem('accountId');
                var fd = new FormData();
                fd.append('AccountId', account);
                fd.append('Search', obj.form.search);
               
                $http({
                    method: 'POST',
                    url: $scope.apiLocation + '/user/MultipleGet',
                    data:fd,
                    headers: {
                        'Content-Type': undefined,
                    }
                }).then(function successCallback(response) {

                    if (response.status == 200) {
                        obj.count = response.data.count;
                        obj.list = response.data.item;
                    } else {
                      
                    }
                   
                }, function errorCallback(response) {
                   
                });
            },


            singleGet: function (id) {
                var a = '';
                $http({
                    method: 'Get',
                    url: $scope.apiLocation + '/user/'+id,
                    headers: {
                        'Content-Type': undefined,
                    }
                }).then(function successCallback(response) {

                    if (response.status == 200) {
                        obj.data = [];
                        obj.data.push( response.data.item);
                        console.log(obj.data);
                        $('#exampleModalupdate').modal('show');
                    } else {

                    }
                  
                }, function errorCallback(response) {

                });
            },
            update: function () {
                var account = localStorage.getItem('accountId');
                var fd = new FormData();

                fd.append('Name', obj.data[0].name);
                fd.append('Id', obj.data[0].id);
                fd.append('Email', obj.data[0].email);
                fd.append('Title', obj.data[0].title);
                fd.append('CompanyName', obj.data[0].companyName);
                fd.append('PhoneNumber', obj.data[0].phoneNumber);
                fd.append('AccountId', account);


                $http({
                    method: 'PUT',
                    url: $scope.apiLocation + '/User',
                    data: fd,
                    headers: {
                        'Content-Type': undefined,

                    }
                }).then(function successCallback(response) {
                    obj.data = [];  
                    obj.data.push(response.data.item);
                    obj.multipleGet();
                    obj.danger = false;
                    obj.succes = true;
                    obj.succesText = {};
                    obj.succesText = response;
                    $('#exampleModalupdate').modal('hide');
                }, function errorCallback(response) {
                    obj.danger = true;
                    obj.succes = false;
                    obj.dangerText = {};
                    obj.dangerText = response;
                });

            },

            delete: function (id) {

                if (confirm('Silmek istediðine emin misin?')) {
                    $http({
                        method: 'DELETE',
                        url: $scope.apiLocation + '/user/' + id ,

                        headers: {
                            'Content-Type': undefined
                        }
                    }).then(function successCallback(response) {
                        if (response.status == 200) {
                            obj.multipleGet();
                        } else {

                        }
                    
                    

                    }, function errorCallback(response) {
                      
                     

                    });

                }
            },
          

        }
        return obj;
    };

    $scope.users = $scope.fnUsers();





    $scope.fnEmails = function () {
        var obj =
        {
            form:
            {
                name: '',
                password: '',
                email: '',
                search: '',
            },
            list: [],
            data: [],
            count: 0,
            danger: false,
            succes: false,
            dangerText: {},
            succesText: {},
            add: function () {
                $('#addModal').modal('show');
            },
            save: function () {

              
                var account = localStorage.getItem('accountId');
                var fd = new FormData();

                fd.append('UserName', obj.form.username);
                fd.append('ServerName', obj.form.serverName);
                fd.append('Password', obj.form.password);
                fd.append('PortNumber', obj.form.portNumber);
                fd.append('AccountId', account);


                $http({
                    method: 'POST',
                    url: $scope.apiLocation + '/Email',
                    data: fd,
                    headers: {
                        'Content-Type': undefined,

                    }
                }).then(function successCallback(response) {
                    obj.data = response.data.item;
                    obj.multipleGet();
                    obj.danger = false;
                    obj.succes = true;
                    obj.succesText = {};
                    obj.succesText = response;
                    $('#addModal').modal('hide');
                }, function errorCallback(response) {
                    obj.danger = true;
                    obj.succes = false;
                    obj.dangerText = {};
                    obj.dangerText = response;
                });

            },

            multipleGet: function () {
                var account = localStorage.getItem('accountId');
                var fd = new FormData();
                fd.append('AccountId', account);
                fd.append('Search', obj.form.search);

                $http({
                    method: 'POST',
                    url: $scope.apiLocation + '/Email/MultipleGet',
                    data: fd,
                    headers: {
                        'Content-Type': undefined,
                    }
                }).then(function successCallback(response) {

                    if (response.status == 200) {
                        obj.count = response.data.count;
                        obj.list = response.data.item;
                    } else {

                    }

                }, function errorCallback(response) {

                });
            },


            singleGet: function (id) {
                var a = '';
                $http({
                    method: 'Get',
                    url: $scope.apiLocation + '/Email/' + id,
                    headers: {
                        'Content-Type': undefined,
                    }
                }).then(function successCallback(response) {

                    if (response.status == 200) {
                        obj.data = [];
                        obj.data.push(response.data.item);
                        console.log(obj.data);
                        $('#emailModalupdate').modal('show');
                    } else {

                    }

                }, function errorCallback(response) {

                });
            },
            update: function () {
                var account = localStorage.getItem('accountId');
                var fd = new FormData();

                fd.append('UserName', obj.data[0].username);
                fd.append('Id', obj.data[0].id);
                fd.append('ServerName', obj.data[0].serverName);
                fd.append('Password', obj.data[0].password);
                fd.append('PortNumber', obj.data[0].portNumber);
                fd.append('AccountId', account);


                $http({
                    method: 'PUT',
                    url: $scope.apiLocation + '/Email',
                    data: fd,
                    headers: {
                        'Content-Type': undefined,

                    }
                }).then(function successCallback(response) {
                    obj.data = [];
                    obj.data.push(response.data.item);
                    obj.multipleGet();
                    obj.danger = false;
                    obj.succes = true;
                    obj.succesText = {};
                    obj.succesText = response;
                    $('#emailModalupdate').modal('hide');
                }, function errorCallback(response) {
                    obj.danger = true;
                    obj.succes = false;
                    obj.dangerText = {};
                    obj.dangerText = response;
                });

            },

            delete: function (id) {

                if (confirm('Silmek istediðine emin misin?')) {
                    $http({
                        method: 'DELETE',
                        url: $scope.apiLocation + '/Email/' + id,

                        headers: {
                            'Content-Type': undefined
                        }
                    }).then(function successCallback(response) {
                        if (response.status == 200) {
                            obj.multipleGet();
                        } else {

                        }



                    }, function errorCallback(response) {



                    });

                }
            },


        }
        return obj;
    };

    $scope.emails = $scope.fnEmails();




    $scope.fnEmailSent = function () {
        var obj =
        {
            form:
            {
                name: '',
                password: '',
                email: '',
                search: '',
                emalName: null,
            },
            list: [],
            users: [],
            data: [],
            count: 0,
            danger: false,
            succes: false,
            dangerText: {},
            succesText: {},
            add: function () {
                $('#addEmailSentModal').modal('show');
            },
            userListAdd: function (userId) {
                var index = obj.users.indexOf(userId); 
                if (index === -1) {
                    obj.users.push(userId);
                } else {
                    obj.users.splice(index, 1);
                }
            },
            save: function () {
                var account = localStorage.getItem('accountId');
                var fd = new FormData();

                fd.append('Content', obj.form.content);
                fd.append('EMailId', obj.form.eMailId);
                fd.append('Title', obj.form.title);
                obj.users.forEach(function (v, i) {

                    fd.append('UserIds[' + i + '].Id', v);

                });
                fd.append('AccountId', account);


                $http({
                    method: 'POST',
                    url: $scope.apiLocation + '/EmailSent',
                    data: fd,
                    headers: {
                        'Content-Type': undefined,

                    }
                }).then(function successCallback(response) {
                    obj.data = response.data.item;
                    obj.multipleGet();
                    obj.danger = false;
                    obj.succes = true;
                    obj.succesText = {};
                    obj.succesText = response;
                    $('#addEmailSentModal').modal('hide');
                }, function errorCallback(response) {
                    obj.danger = true;
                    obj.succes = false;
                    obj.dangerText = {};
                    obj.dangerText = response;
                    console.log(obj.dangerText.data);
                });

            },

            multipleGet: function () {
                var account = localStorage.getItem('accountId');
                var fd = new FormData();
                fd.append('AccountId', account);
                fd.append('Search', obj.form.search);

                $http({
                    method: 'POST',
                    url: $scope.apiLocation + '/EmailSent/MultipleGet',
                    data: fd,
                    headers: {
                        'Content-Type': undefined,
                    }
                }).then(function successCallback(response) {

                    if (response.status == 200) {
                        obj.count = response.data.count;
                        obj.list = response.data.item;
                    } else {

                    }

                }, function errorCallback(response) {

                });
            },


            singleGet: function (id) {
                var a = '';
                $http({
                    method: 'Get',
                    url: $scope.apiLocation + '/EmailSent/' + id,
                    headers: {
                        'Content-Type': undefined,
                    }
                }).then(function successCallback(response) {

                    if (response.status == 200) {
                        obj.data = [];
                        obj.data.push(response.data.item);
                        console.log(obj.data);
                        $('#emailSentModalupdate').modal('show');
                    } else {

                    }

                }, function errorCallback(response) {

                });
            },
   

            delete: function (id) {

                if (confirm('Silmek istediðine emin misin?')) {
                    $http({
                        method: 'DELETE',
                        url: $scope.apiLocation + '/EmailSent/' + id,

                        headers: {
                            'Content-Type': undefined
                        }
                    }).then(function successCallback(response) {
                        if (response.status == 200) {
                            obj.multipleGet();
                        } else {

                        }



                    }, function errorCallback(response) {



                    });

                }
            },


        }
        return obj;
    };

    $scope.emailSent = $scope.fnEmailSent();



});
