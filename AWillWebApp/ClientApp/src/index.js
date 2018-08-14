import React from 'react'
import ReactDOM from 'react-dom'

import App from './App'

import registerServiceWorker from './registerServiceWorker'

import 'bootstrap/dist/css/bootstrap.css'
import 'bootstrap/dist/css/bootstrap-theme.css'
//import './index.css'
import '../css/site.css'

const rootElement = document.getElementById('root')

ReactDOM.render(<App />, rootElement)

registerServiceWorker()
