import { Component, OnInit } from '@angular/core';
import {TodoDto} from '../shared/todo.dto';
import {HttpClient} from '@angular/common/http';
import {UserDto} from '../shared/user.dto';

@Component({
  selector: 'app-todos-overview',
  templateUrl: './todos-overview.component.html',
  styleUrls: ['./todos-overview.component.scss']
})
export class TodosOverviewComponent implements OnInit {
  todos: TodoDto[] = [];
  users: UserDto[] = [];
  userId: number = 1;

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    this.http.get<TodoDto[]>('https://jsonplaceholder.typicode.com/todos?userId=' + this.userId)
      .subscribe(todosArray => {
        this.todos = todosArray;
      });

    this.http.get<UserDto[]>('https://jsonplaceholder.typicode.com/users')
      .subscribe(usersArray => {
        this.users = usersArray;
      });
  }

  getTodosForUser(): void {
    this.http
      .get<TodoDto[]>('https://jsonplaceholder.typicode.com/todos?userId=' + this.userId)
      .subscribe(todosArray => {
        this.todos = todosArray;
      });
  }
}
