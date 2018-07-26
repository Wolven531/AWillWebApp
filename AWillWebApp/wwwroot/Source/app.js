"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
// TODO: find a way to avoid including
// jquery in app entry as follows
var jQuery = require("jquery");
var $ = window.$ = window.jQuery = jQuery;
var React = require("react");
var ReactDOM = require("react-dom");
var lib_1 = require("./lib");
var es6lib_1 = require("./es6lib");
var counter_1 = require("./counter");
var fetchdata_1 = require("./fetchdata");
//import 'bootstrap/dist/css/bootstrap.min.css'
require("../css/site.css");
// NOTE: vanilla JS
document.getElementById('fillthis').appendChild(document.createTextNode(lib_1.getText()));
// NOTE: jQuery
$('#fillthiswithjquery').html('Filled by Jquery!');
// NOTE: ES6
var myES6Object = new es6lib_1.default();
$('#fillthiswithes6lib').html(myES6Object.getData());
// NOTE: ReactJS
ReactDOM.render(React.createElement(counter_1.default, null), document.getElementById('basicreactcomponent'));
// NOTE: ReactJS with API
ReactDOM.render(React.createElement(fetchdata_1.default, null), document.getElementById('reactcomponentwithapidata'));
//# sourceMappingURL=app.js.map