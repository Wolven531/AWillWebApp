import * as React from 'react'
import { connect } from 'react-redux'

import { MonsterCollectionView } from '../../components/MonsterCollectionView/MonsterCollectionView'
import { userActions, userConstants } from '../UserActions'

class HomePage extends React.Component<{ dispatch: ((action: any) => void) }> {
	public componentDidMount() {
		console.info('[HomePage | componentDidMount]')
	}

	public render() {
		return (
			<div>
				<h1>Homepage</h1>
				<button onClick={this.handleLogout}>Logout</button>
				<MonsterCollectionView />
			</div>
		)
	}

	private handleLogout = () => {
		this.props.dispatch(userActions.logout())
		window.location.reload()
	}
}

const connectedHomePage = connect()(HomePage)

export { connectedHomePage as HomePage }
