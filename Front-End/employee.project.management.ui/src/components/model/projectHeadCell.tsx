import { ProjectModel } from "./projectModel";

export type ProjectHeadCell = {
    id: keyof ProjectModel;
    label: string;
};
