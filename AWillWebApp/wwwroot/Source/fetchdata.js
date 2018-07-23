"use strict";
var __extends = (this && this.__extends) || (function () {
    var extendStatics = Object.setPrototypeOf ||
        ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
        function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();
Object.defineProperty(exports, "__esModule", { value: true });
var React = require("react");
require("es6-promise");
require("isomorphic-fetch");
var FetchData = /** @class */ (function (_super) {
    __extends(FetchData, _super);
    function FetchData(props) {
        var _this = _super.call(this, props) || this;
        _this.refreshData = function () {
            fetch('api/SampleDummyData/')
                .then(function (response) { return response.json(); })
                .then(function (apiDataObjects) {
                _this.setState({
                    apiDataObjects: apiDataObjects,
                    loading: false
                });
            });
        };
        _this.state = {
            apiDataObjects: [],
            loading: true
        };
        _this.refreshData();
        return _this;
    }
    FetchData.prototype.render = function () {
        var _this = this;
        var contents = this.state.loading ?
            (React.createElement("p", null,
                React.createElement("em", null, "Loading...")))
            : (React.createElement("textarea", null, JSON.stringify(this.state.apiDataObjects, null, 4)));
        return (React.createElement("div", null,
            React.createElement("h1", null, "API Check"),
            React.createElement("p", null, "This component demonstrates fetching data from the server"),
            React.createElement("button", { onClick: function () { return _this.refreshData; } }, "Refresh"),
            contents));
    };
    return FetchData;
}(React.Component));
exports.default = FetchData;
//# sourceMappingURL=fetchdata.js.map