(function () {
    var wmiSvc = function ($http, $q) {
        var
            queryModel = {
                query: '',
                properties: [],
                computername: '',
                wmiNamespace: 'root\cimv2',
                results: ''
            },
            queryWmi = function (model) {
                var deferred = $q.defer();

                $http({
                    url: '/api/wmi/queryWmi',
                    method: 'POST',
                    data: model
                }).success(function (data) {
                    queryModel = data;
                    deferred.resolve();
                }).error(function (err) {
                    if (err.ExceptionMessage) {
                        console.error(err.ExceptionMessage);
                        queryModel.results = err.ExceptionMessage;
                    } else if (err.Message) {
                        console.error(err.Message);
                        queryModel.results = err.Message;
                    } else if (err) {
                        console.error(err);
                        queryModel.results = err;
                    } else {
                        console.error("Unspecified error interfacing with the PowerShell API");
                        queryModel.results = "Unspecified error interfacing with the PowerShell API";
                    }

                    deferred.reject(err);
                });

                return deferred.promise;
            };

        return {
            queryModel: queryModel,
            queryWmi: queryWmi
        };
    };

    wmiSvc.$inject = ['$http', '$q'];
    powerShellApp.factory('wmiSvc', wmiSvc);
}());