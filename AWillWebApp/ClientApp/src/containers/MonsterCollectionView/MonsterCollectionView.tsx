import * as React from 'react'
import { connect } from 'react-redux'

import { actionCreators as monsterActions } from '../../store/Monster'

import { Monster } from '../../models/monster'
import { MonsterView } from '../../components/MonsterView/MonsterView'

const NoMonsterDisplay = () => <div>You have no monsters!</div>

class MonsterCollectionView extends React.Component<{
	monsters?: Monster[]
	dispatch: ((action: any) => void)
}> {
	public componentDidMount() {
		console.info('[MonsterCollectionView | componentDidMount]')
		this.props.dispatch(monsterActions.loadMonsters())
	}

	public render() {
		console.info('[MonsterCollectionView | render]')
		if (!this.props.monsters || this.props.monsters.length < 1) {
			return <NoMonsterDisplay />
		}
		return (
			<div>
				<h2>Monster Collection View</h2>
				{this.props.monsters.map(monster => (
					<MonsterView monster={monster} key={`${monster.element}-${monster.name}-${monster.awakenedName}`} />
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
