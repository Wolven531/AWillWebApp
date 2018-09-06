import * as React from 'react'
import { connect } from 'react-redux'

// import { actionCreators as monsterActions } from '../../store/Monster'

// import { MonsterCollectionView } from '../MonsterCollectionView/MonsterCollectionView'

class MyMonstersPage extends React.Component<{ dispatch: ((action: any) => void) }> {
	public componentDidMount() {
		console.info('[MyMonstersPage | componentDidMount]')
		// this.props.dispatch(monsterActions.loadMonstersFromApi())
	}

	public render() {
		console.info('[MyMonstersPage | render]')
		return (
			<div id="my-monsters-page">
				<h1>My Monsters</h1>
			</div>
		)
	}
}

const connectedMyMonstersPage = connect()(MyMonstersPage)

export { connectedMyMonstersPage as MyMonstersPage }
