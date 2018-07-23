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
define("lib", ["require", "exports"], function (require, exports) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    exports.getText = function () { return 'Data from getText function in dep.js'; };
});
// export { getText }
define("es6codelib", ["require", "exports"], function (require, exports) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    var ES6Lib = /** @class */ (function () {
        function ES6Lib() {
            var _this = this;
            this.getData = function () { return _this._text; };
            this._text = 'Data from ES6 class';
        }
        return ES6Lib;
    }());
    exports.default = ES6Lib;
});
define("reactcomponent", ["require", "exports", "react"], function (require, exports, React) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
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
});
define("fetchdata", ["require", "exports", "react", "es6-promise", "isomorphic-fetch"], function (require, exports, React) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
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
});
//// TODO: find a way to avoid including
//// jquery in app entry as follows
//import * as jQuery from 'jquery'
//const $ = (window as any).$ = (window as any).jQuery = jQuery
define("app", ["require", "exports", "react", "react-dom", "lib", "reactcomponent", "fetchdata", "bootstrap/dist/css/bootstrap.min.css", "../css/site.css"], function (require, exports, React, ReactDOM, lib_1, reactcomponent_1, fetchdata_1) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    // NOTE: vanilla JS
    document.getElementById('fillthis').appendChild(document.createTextNode(lib_1.getText()));
    //// NOTE: jQuery
    //$('#fillthiswithjquery').html('Filled by Jquery!')
    //// NOTE: ES6
    //const myES6Object = new ES6Lib()
    //$('#fillthiswithes6lib').html(myES6Object.getData())
    // NOTE: ReactJS
    ReactDOM.render(React.createElement(reactcomponent_1.default, null), document.getElementById('basicreactcomponent'));
    // NOTE: ReactJS with API
    ReactDOM.render(React.createElement(fetchdata_1.default, null), document.getElementById('reactcomponentwithapidata'));
});
//# sourceMappingURL=bundle.js.map