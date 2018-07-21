﻿// TODO: find a way to avoid including
// jquery in app entry as follows
import * as jQuery from 'jquery'
const $ = (window as any).$ = (window as any).jQuery = jQuery

import { getText } from './lib'
import ES6Lib from './es6codelib'

// NOTE: vanilla JS
document.getElementById('fillthis').appendChild(document.createTextNode(getText()))

// NOTE: jQuery
$('#fillthiswithjquery').html('Filled by Jquery!')

// NOTE: ES6
const myES6Object = new ES6Lib()
$('#fillthiswithes6lib').html(myES6Object.getData())
