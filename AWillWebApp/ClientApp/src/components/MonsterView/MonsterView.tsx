import * as React from 'react'

import { Monster } from '../../models/monster'

import './MonsterView.css'

class MonsterView extends React.Component<{ monster: Monster }> {
	public render() {
		const { monster } = this.props
		return (
			<div className={`monster-view ${monster.element.toLowerCase()}`}>
				<h3>
					{monster.awakenedName} ({monster.element} {monster.name})
				</h3>
				<section className="monster-images">
					<img
						// src={`data:image/png;base64,${monster.image}`}
						src={`data:image/png;base64,undefined`}
						alt={`Image of monster - ${monster.element} ${monster.name}`}
						title={`Image of monster - ${monster.element} ${monster.name}`}
					/>
					<img
						// src={`data:image/png;base64,${monster.awakenedImage}`}
						src={`data:image/png;base64,undefined`}
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

export { MonsterView }
