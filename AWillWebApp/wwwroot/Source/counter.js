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
var Counter = /** @class */ (function (_super) {
    __extends(Counter, _super);
    function Counter(props) {
        var _this = _super.call(this, props) || this;
        _this.incrementCount = function () { return _this.setState({ count: _this.state.count + 1 }); };
        _this.state = {
            count: 0
        };
        return _this;
    }
    Counter.prototype.render = function () {
        return (React.createElement("div", null,
            React.createElement("h1", null, "Counter"),
            React.createElement("p", null, "This is a simple example of a React component"),
            React.createElement("p", null,
                "Current count: ",
                React.createElement("strong", null, this.state.count)),
            React.createElement("button", { onClick: this.incrementCount }, "Increment")));
    };
    return Counter;
}(React.Component));
exports.default = Counter;
//# sourceMappingURL=counter.js.map