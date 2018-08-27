import * as React from 'react'
import { connect } from 'react-redux'

import { actionCreators as monsterActions } from '../../store/Monster'

import { Monster } from '../../models/monster'

import './MonsterView.css'

class MonsterView extends React.Component<{
	dispatch: ((action: any) => void)
	monster: Monster
	monsterWithImage: Monster | undefined
}> {
	public componentDidMount() {
		// console.info('[MonsterView | componentDidMount]')
		// if (!this.props.monster.image || this.props.monster.image === '') {
		// 	this.props.dispatch(monsterActions.loadMonsterWithImages(this.props.monster.id))
		// }
	}

	public componentWillMount() {
		// console.info('[MonsterView | componentWillMount]')
		// if (!this.props.monster.image || this.props.monster.image === '') {
		// 	this.props.dispatch(monsterActions.loadMonsterWithImages(this.props.monster.id))
		// }
	}

	public shouldComponentUpdate(nextProps: any, nextState: any, nextContext: any): boolean {
		// console.log(`[shouldComponentUpdate | MonsterView] ${JSON.stringify(nextProps)}`)
		return false
	}

	public render() {
		console.info('[MonsterView | render]')
		const { monster, monsterWithImage } = this.props
		return (
			<div className={`monster-view ${monster.element.toLowerCase()}`}>
				<h3>
					{monster.awakenedName} ({monster.element} {monster.name})
				</h3>
				<section className="monster-images">
					<img
						src={`data:image/png;base64,${monsterWithImage && monsterWithImage.image}`}
						alt={`Image of monster - ${monster.element} ${monster.name}`}
						title={`Image of monster - ${monster.element} ${monster.name}`}
					/>
					<img
						src={`data:image/png;base64,${monsterWithImage && monsterWithImage.awakenedImage}`}
						alt={`Image of awakened monster - ${monster.awakenedName}`}
						title={`Image of awakened monster - ${monster.awakenedName}`}
					/>
				</section>
				<section className="monster-stats">
					<p className="monster-stat-label">Rating</p>
					<p className="monster-stat">{monster.rating} ⭐</p>
					<p className="monster-stat-label">Early Runes</p>
					<p className="monster-stat">{monster.earlyRuneList || '-'}</p>
					<p className="monster-stat-label">Early Rune Stats</p>
					<p className="monster-stat">{monster.earlyRuneValues || '-'}</p>
					<p className="monster-stat-label">Late Runes</p>
					<p className="monster-stat">{monster.lateRuneList || '-'}</p>
					<p className="monster-stat-label">Late Rune Stats</p>
					<p className="monster-stat">{monster.lateRuneValues || '-'}</p>
					<p className="monster-stat-label">Stat Priority</p>
					<p className="monster-stat">{monster.statPriority || '-'}</p>
				</section>
			</div>
		)
	}
}

const mapStateToProps = (state: any) => {
	const { monster } = state
	const { monsterWithImage } = monster

	return {
		monsterWithImage
	}
}

const connectedMonsterView = connect(mapStateToProps)(MonsterView)

export { connectedMonsterView as MonsterView }
