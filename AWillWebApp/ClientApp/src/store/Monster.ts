const LOAD_MONSTERS = 'load_monsters'

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
	monsters: any[]
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
	loadMonsters: (): IMonsterReducerAction => ({
		payload: { },
		type: LOAD_MONSTERS
	})
}

const reducer = (
	state: Partial<IMonsterState> = initialState,
	action: IMonsterReducerAction
) => {
	const { payload, type } = action

	if (type === LOAD_MONSTERS) {
		console.info(`[Monster | reducer | LOAD_MONSTERS]`)
		return {
			...state,
			monsters: [{ name: 'mon 1' }]
		}
	}

	return state
}

export {
	actionCreators,
	IMonsterReducerAction,
	reducer
}
