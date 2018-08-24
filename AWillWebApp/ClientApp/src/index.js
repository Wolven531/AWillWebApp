import React from 'react'
import ReactDOM from 'react-dom'

import App from './App'

import registerServiceWorker from './registerServiceWorker'

//import './index.css'
import '../css/site.css'

const rootElement = document.getElementById('root')

ReactDOM.render(<App />, rootElement)

registerServiceWorker()
