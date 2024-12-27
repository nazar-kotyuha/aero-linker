import { Option } from 'src/app/models/drone/option';

export interface Command {
    option: Option;
    step: number;
}
