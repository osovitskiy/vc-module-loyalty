angular.module('virtoCommerce.loyaltyModule')
.factory('virtoCommerce.loyaltyModule.statuses', ['$resource', function ($resource) {
        return $resource('api/loyalty/statuses', {}, {
            all: {},
            get: { url: 'api/loyalty/statuses/:id' },
            update: { method: 'PUT' }
    });
}]);
