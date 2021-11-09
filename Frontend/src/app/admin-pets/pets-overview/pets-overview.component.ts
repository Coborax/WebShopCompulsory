import { Component, OnInit } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {PetDto} from '../shared/pet.dto';

@Component({
  selector: 'app-pets-overview',
  templateUrl: './pets-overview.component.html',
  styleUrls: ['./pets-overview.component.scss']
})
export class PetsOverviewComponent implements OnInit {
  pets: PetDto[] | undefined;

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.http.get<PetDto[]>(
      'https://localhost:5001/api/Pets?Limit=10&Page=1&OrderDir=asc&OrderBy=name&Search=n'
    ).subscribe(pets => {
      this.pets = pets;
    })
  }

}
