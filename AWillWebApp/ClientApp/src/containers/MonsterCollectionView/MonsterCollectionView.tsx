import * as React from 'react'
import { connect } from 'react-redux'

import { Monster } from '../../models/monster'
import { MonsterView } from '../MonsterView/MonsterView'

import './MonsterCollectionView.css'

const NoMonsterDisplay = () => <div>You have no monsters!</div>

class MonsterCollectionView extends React.Component<{
	monsters?: Monster[]
	dispatch: ((action: any) => void)
}> {
	public componentDidMount() {
		console.info('[MonsterCollectionView | componentDidMount]')
	}

	public render() {
		console.info('[MonsterCollectionView | render]')
		if (!this.props.monsters || this.props.monsters.length < 1) {
			return <NoMonsterDisplay />
		}
		return (
			<div className="monster-collection-view">
				{this.props.monsters.map(monster => (
					<MonsterView
						key={`${monster.element}-${monster.name}-${monster.awakenedName}`}
						monster={monster}
						/>
				))}
			</div>
		)
	}
}

const mapStateToProps = (state: any) => {
	const { monster } = state
	const { monsters } = monster
	console.info(`[MonsterCollectionView | mapStateToProps] Num monsters = ${monsters.length}`)

	return {
		monsters
	}
}

const connectedMonsterCollectionView = connect(mapStateToProps)(MonsterCollectionView)

export { connectedMonsterCollectionView as MonsterCollectionView }
