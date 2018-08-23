import * as React from 'react'
import { connect } from 'react-redux'

import { MonsterView } from '../MonsterView/MonsterView'

const NoMonsterDisplay = () => <div>You have no monsters!</div>

class MonsterCollectionView extends React.Component<{ monsters?: any[] }> {
	public componentDidMount() {
		console.info('[MonsterCollectionView | componentDidMount]')
	}

	public render() {
		if (!this.props.monsters || this.props.monsters.length < 1) {
			return <NoMonsterDisplay />
		}
		return (
			<div>
				<h2>Monster Collection View</h2>
				{this.props.monsters.map((monster: any) => (
					<MonsterView monster={monster} />
				))}
			</div>
		)
	}
}

const connectedMonsterCollectionView = connect()(MonsterCollectionView)

export { connectedMonsterCollectionView as MonsterCollectionView }
