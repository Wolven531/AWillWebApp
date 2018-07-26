/// <reference types="react" />
import * as React from 'react';
export interface ICounterState {
    count: number;
}
export default class Counter extends React.Component<null, ICounterState> {
    constructor(props: any);
    render(): JSX.Element;
    private incrementCount;
}
