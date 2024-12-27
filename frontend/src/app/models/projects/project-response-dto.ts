import { TagDto } from '../tags/tag-dto';

export interface ProjectResponseDto {
    id: number;
    isAuthor: boolean;
    name: string;
    description: string | null;
    createdAt: Date;

    tags: TagDto[];
}
