﻿namespace Simple.Repository.Models.System;

/// <summary>
/// 角色菜单关联表
/// </summary>
public class SysRoleMenu : EntityBase
{
    /// <summary>
    /// 角色Id
    /// </summary>
    public Guid RoleId { get; set; }

    /// <summary>
    /// 菜单Id
    /// </summary>
    public Guid MenuId { get; set; }

    /// <summary>
    /// 角色
    /// </summary>
    public SysRole? Role { get; set; }

    /// <summary>
    /// 菜单
    /// </summary>
    public SysMenu? Menu { get; set; }

    public override void ConfigureEntity(ModelBuilder builder)
    {
        // 复合主键
        builder.Entity<SysRoleMenu>()
            .HasKey(e => new { e.RoleId, e.MenuId });

        // Index
        builder.Entity<SysRoleMenu>()
            .HasIndex(e => new { e.MenuId, e.RoleId })
            .IsUnique(false);

        // 关系配置
        builder.Entity<SysRoleMenu>()
            .HasOne(rm => rm.Role)
            .WithMany(r => r.RoleMenus)
            .IsRequired(false); // 消除 https://go.microsoft.com/fwlink/?linkid=2131316
        builder.Entity<SysRoleMenu>()
            .HasOne(rm => rm.Menu)
            .WithMany(r => r.RoleMenus)
            .IsRequired(false);
    }
}
