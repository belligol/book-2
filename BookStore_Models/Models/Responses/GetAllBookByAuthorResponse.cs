using BookStore_Models.Models;
using System;

public class GetAllBookByAuthorResponse
{
	public Author ?Author {  get; set; }	
	public List<Book> ?Books { get; set; }
 }
