(function () {
    var wmiSvc = function ($http, $q, utilitySvc, toastrSvc) {
        var
            queryModel = {
                query: '',
                computername: '',
                wmiNamespace: 'root\\cimv2',
                username: '',
                password: '',
                isRemoteConnection: false
            },
            resultsModel = {
                results: []
            },
            propertiesModel = {
                properties: []
            },
            queryWmi = function (model) {
                var deferred = $q.defer();
                
                $http({
                    url: '/api/wmi/queryWmi',
                    method: 'POST',
                    data: model
                }).success(function (data) {
                    resultsModel.results = data.results;
                    propertiesModel.properties = data.properties;
                    if (resultsModel.results.length < 1) {
                        toastrSvc.alertWarning("The provided query completed successfully, but did not return any results");
                    }
                    deferred.resolve();
                }).error(function (err) {
                    console.log(err);
                    utilitySvc.toastErrorMessage(err, "Error Querying WMI");
                    deferred.reject(err);
                });

                return deferred.promise;
            };

        return {
            queryModel: queryModel,
            resultsModel: resultsModel,
            propertiesModel: propertiesModel,
            queryWmi: queryWmi
        };
    };

    wmiSvc.$inject = ['$http', '$q', 'utilitySvc', 'toastrSvc'];
    powershellApp.factory('wmiSvc', wmiSvc);
}());