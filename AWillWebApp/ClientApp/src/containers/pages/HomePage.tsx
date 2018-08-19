import * as React from 'react'
import { connect } from 'react-redux'

import { userActions, userConstants } from '../UserActions'

const HomePage = (props: any) => {
	const handleLogout = () => {
		props.dispatch(userActions.logout())
		window.location.reload()
	}

	return (
		<div>
			<h2>Homepage</h2>
			<button onClick={handleLogout}>Logout</button>
		</div>
	)
}

const connectedHomePage = connect()(HomePage)

export { connectedHomePage as HomePage }
