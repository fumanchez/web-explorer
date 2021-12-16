import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';

import {environment} from '../environments/dev.environment';

export type File = {
    path: string;
    contentType: string;
};

export type Directory = {
    path: string;
    parent?: Directory;
};

@Injectable({
    providedIn: 'root'
})
export class NavigationService {
    private dirsUrl = new URL('dirs', environment.apiUrl);
    private filesUrl = new URL('files', environment.apiUrl);

    constructor(private http: HttpClient) {}

    getFile(path: string) {
        path = encodeURIComponent(path);
        let url = `${this.filesUrl.href}/${path}`;
        let content = this.http.get(url);
        return content;
    }

    getFiles(path: string) {
        path = encodeURIComponent(path);
        let url = `${this.filesUrl.href}/${path}/*`;
        let files = this.http.get<File[]>(url);
        return files;
    }

    getDirectories(path: string) {
        path = encodeURIComponent(path);
        let url = `${this.dirsUrl.href}/${path}`;
        let dirs = this.http.get<Directory[]>(url);
        return dirs;
    }
}
