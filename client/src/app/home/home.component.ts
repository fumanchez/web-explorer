import {Component} from '@angular/core';

import {File, Directory, NavigationService} from '../navigation.service';

@Component({
    selector: 'app-home',
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.css']
})
export class HomeComponent {
    private readonly rootDir: Directory = {path: ""};

    readonly dirs: Directory[] = [];
    readonly files: File[] = [];

    readonly history: Directory[] = [];

    activeDir: Directory = this.rootDir;

    constructor(private navigationService: NavigationService) {
        this.goTo(this.rootDir);
    }

    goUp() {
        if (this.activeDir.parent) this.goTo(this.activeDir.parent);
        else this.goHome();
    }

    goBack() { // FIXME
        if (this.history.length < 2) return;

        let previousDir = this.history[this.history.length - 2];
        this.goTo(previousDir);
    }

    goHome() {
        this.goTo(this.rootDir);
    }

    refresh() {
        this.goTo(this.activeDir);
    }

    goTo(dir: Directory) {
        this.activeDir = dir;

        this.clean();
        this.update(dir);

        this.navigationService.getDirectories(dir.path).subscribe(dirs => {
            dirs.forEach(subdir => this.dirs.push(subdir));
        });

        this.navigationService.getFiles(dir.path).subscribe(files => {
            files.forEach(file => this.files.push(file));
        });

        // TODO: add event
        this.onDirectoryChanged();
    }

    open(file: File) {
        return this.navigationService.getFile(file.path);
    }

    private clean() {
        while (this.files.length > 0) this.files.pop();
        while (this.dirs.length > 0) this.dirs.pop();
    }

    private update(dir: Directory) {
        this.activeDir = dir;
    }

    private onDirectoryChanged() {
        this.history.push(this.activeDir);
    }
}
