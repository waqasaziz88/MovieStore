﻿using System;
using System.Text;
using Domain;
using Domain.Repositories;
using Domain.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;


namespace MovieStore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IMovieRepository, MovieRepository>();
            services.AddScoped<IMovieRatingRepository, MovieRatingRepository>();

            services.AddMvc();

            services.AddDbContext<MovieStoreDbContext>(opt =>
            {
                opt.UseSqlServer(Configuration["DefalutConnectionString"]);
            });

        }


        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseAuthentication();


            app.UseMvc();
        }
    }
}
