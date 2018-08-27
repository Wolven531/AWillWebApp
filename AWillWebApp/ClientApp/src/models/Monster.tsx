class Monster {
	public id: string = ''

	constructor(
		public name: string,
		public awakenedName: string,
		public element: string,
		public rating: number,
		public earlyRuneList: string,
		public earlyRuneValues: string,
		public lateRuneList: string,
		public lateRuneValues: string,
		public statPriority: string,
		public awakenedImage: string,
		public image: string
	) {}
}

export { Monster }
