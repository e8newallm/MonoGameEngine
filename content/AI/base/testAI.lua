SCRIPTTYPE = "AI"

function init()
    print("INIT")
end

function update(terrain, gameTime)
    if this:OnGround(terrain) then
        this.YVel = -1.0;
    end
    this.XVel = 1.0;
end

function deinit()
    print("DEINIT")
end