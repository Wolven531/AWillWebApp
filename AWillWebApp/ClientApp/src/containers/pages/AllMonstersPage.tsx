import * as React from 'react'
import { connect } from 'react-redux'

import { actionCreators as monsterActions } from '../../store/Monster'

import { MonsterCollectionView } from '../MonsterCollectionView/MonsterCollectionView'

class AllMonstersPage extends React.Component<{ dispatch: ((action: any) => void) }> {
	public componentDidMount() {
		console.info('[AllMonstersPage | componentDidMount]')
		this.props.dispatch(monsterActions.loadMonsters())
	}

	public render() {
		console.info('[AllMonstersPage | render]')
		return (
			<div id="all-monster-page">
				<h1>All Monsters</h1>
				<MonsterCollectionView />
			</div>
		)
	}
}

const connectedAllMonstersPage = connect()(AllMonstersPage)

export { connectedAllMonstersPage as AllMonstersPage }
