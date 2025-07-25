﻿using MyRecipeBook.Domain.Repositories;

namespace MyRecipeBook.Infrastructure.DataAcess;
public class UnitOfWork : IUnitOfWork
{
    private readonly MyRecipeBookDbContext _context;

    public UnitOfWork(MyRecipeBookDbContext context)
    {
        _context = context;
    }

    public async Task Commit() => await _context.SaveChangesAsync();
}
