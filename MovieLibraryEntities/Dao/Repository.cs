﻿using Microsoft.EntityFrameworkCore;
using MovieLibraryEntities.Context;
using MovieLibraryEntities.Models;

namespace MovieLibraryEntities.Dao;

public class Repository : IRepository, IDisposable
{
    private readonly MovieContext _context;
    private readonly IDbContextFactory<MovieContext> _contextFactory;

    public Repository()
    {
        _context = new MovieContext();
    }

    public Repository(IDbContextFactory<MovieContext> contextFactory)
    {
        _contextFactory = contextFactory;
        _context = _contextFactory.CreateDbContext();
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    public IEnumerable<Movie> GetAll()
    {
        return _context.Movies.ToList();
    }

    public IEnumerable<Movie> Search(string searchString)
    {
        var allMovies = _context.Movies;
        var listOfMovies = allMovies.ToList();
        var temp = listOfMovies.Where(x => x.Title.Contains(searchString, StringComparison.CurrentCultureIgnoreCase));

        return temp;
    }
}
