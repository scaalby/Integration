var CorporationsServices = angular.module('CorporationsServices', ['ngResource']);

CorporationsServices.factory('RealEstates', ['$resource',
  function($resource){
    return $resource('http://192.168.3.68:59536/Example/GetRealEstates/:corporationId', {}, {
      query: {method:'GET', params:{corporationId:'corporation_id'}, isArray:true}
    });
  }]);
