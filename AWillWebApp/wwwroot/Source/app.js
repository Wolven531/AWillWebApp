"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
// TODO: find a way to avoid including
// jquery in app entry as follows
var jQuery = require("jquery");
var $ = window.$ = window.jQuery = jQuery;
var lib_1 = require("./lib");
var es6codelib_1 = require("./es6codelib");
// NOTE: vanilla JS
document.getElementById('fillthis').appendChild(document.createTextNode(lib_1.getText()));
// NOTE: jQuery
$('#fillthiswithjquery').html('Filled by Jquery!');
// NOTE: ES6
var myES6Object = new es6codelib_1.default();
$('#fillthiswithes6lib').html(myES6Object.getData());
//# sourceMappingURL=app.js.map