import { Monster } from '../models/monster'

const REQUEST_MONSTERS = 'request_monsters'
const RECEIVE_MONSTERS = 'receive_monsters'

/*
	This interface is used to represent the action this reducer is capable
	of processing
*/
interface IMonsterReducerAction {
	payload: any
	type: string
}

/*
	This interface is used to represent the state maintained by this reducer
*/
interface IMonsterState {
	monsters: Monster[]
}

/*
	This is the initial state for this reducer
*/
const initialState: IMonsterState = {
	monsters: []
}

/*
	This is a map of functions that create actions which may be processed by
	this reducer
*/
const actionCreators = {
	loadMonstersFromApi: () => async (dispatch: (action: IMonsterReducerAction) => void) => {
		dispatch({ type: REQUEST_MONSTERS, payload: {} })

		const url = `api/monsters`
		const response = await fetch(url)
		const monsters: Monster[] = await response.json()

		dispatch({ type: RECEIVE_MONSTERS, payload: { monsters } })
	}
}

const reducer = (state: Partial<IMonsterState> = initialState, action: IMonsterReducerAction) => {
	const { payload, type } = action

	if (type === RECEIVE_MONSTERS) {
		const monsters: Monster[] = payload.monsters
		console.info(`[Monster | reducer | RECEIVE_MONSTERS] Number monsters = ${monsters.length}`)

		return {
			...state,
			monsters
		}
	}

	return state
}

export { actionCreators, IMonsterReducerAction, reducer }
